using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Domain.Model;
using MyEshop_Phone.Infra.Data.Context;

namespace MyEshop_Phone.Seeding;

public static class WorkflowPreviewDataSeeder
{
    private const string ProductPrefix = "WF-TEST-";

    public static async Task SeedAsync(MyDbContext db, ILogger logger, CancellationToken cancellationToken = default)
    {
        if (!await db.Database.CanConnectAsync(cancellationToken))
        {
            logger.LogWarning("Workflow preview seeding skipped: database is not reachable.");
            return;
        }

        await using var tx = await db.Database.BeginTransactionAsync(cancellationToken);

        await ClearPreviousWorkflowSeedAsync(db, cancellationToken);

        var groupIds = await EnsureGroupsAsync(db, cancellationToken);
        var submenuIds = await EnsureSubmenusAsync(db, groupIds, cancellationToken);
        var featureIds = await EnsureFeaturesAsync(db, cancellationToken);
        var colorIds = await EnsureColorsAsync(db, cancellationToken);

        var seeds = BuildProductSeeds();
        var products = new List<_Products>(seeds.Count);

        for (var i = 0; i < seeds.Count; i++)
        {
            var seed = seeds[i];
            var title = $"{ProductPrefix}{(i + 1):00} - {seed.Title}";
            var submenuKey = BuildSubmenuKey(seed.GroupTitle, seed.SubmenuTitle);

            products.Add(new _Products
            {
                ProductGroupsId = groupIds[seed.GroupTitle],
                SubmenuGroupsId = submenuIds[submenuKey],
                Title = title,
                ShortDescription = seed.Brand,
                Text = seed.DescriptionText,
                Price = seed.Price,
                ImageName = "8.jpg",
                CreateTime = DateTime.UtcNow.AddMinutes(-((i + 1) * 5)),
                Count = seed.Count,
                RecommendedProducts = seed.Recommended
            });
        }

        await db.Products.AddRangeAsync(products, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        var seededProducts = await db.Products
            .Where(p => p.Title.StartsWith(ProductPrefix))
            .OrderBy(p => p.Title)
            .ToListAsync(cancellationToken);

        var galleryImage = "10cb4dd6-f540-4cdb-8801-11f0e36a2642.jpg";
        var colorPalette = colorIds.Values.ToList();

        var tags = new List<_Products_Tags>();
        var galleries = new List<_Products_Galleries>();
        var productFeatures = new List<_Products_Features>();
        var productColors = new List<_ProductsColor>();
        var comments = new List<_Products_comment>();

        for (var i = 0; i < seededProducts.Count; i++)
        {
            var product = seededProducts[i];
            var seed = seeds[i];

            tags.Add(new _Products_Tags { ProductsId = product.Id, Tag = "تستی" });
            tags.Add(new _Products_Tags { ProductsId = product.Id, Tag = seed.SubmenuTitle });
            tags.Add(new _Products_Tags { ProductsId = product.Id, Tag = seed.Brand });

            galleries.Add(new _Products_Galleries
            {
                ProductsId = product.Id,
                Title = $"گالری {seed.Title}",
                ImageName = galleryImage
            });

            productFeatures.Add(new _Products_Features
            {
                ProductsId = product.Id,
                FeaturesId = featureIds["برند"],
                Value = seed.Brand
            });
            productFeatures.Add(new _Products_Features
            {
                ProductsId = product.Id,
                FeaturesId = featureIds["گارانتی"],
                Value = seed.Warranty
            });
            productFeatures.Add(new _Products_Features
            {
                ProductsId = product.Id,
                FeaturesId = featureIds["جنس"],
                Value = seed.Material
            });
            productFeatures.Add(new _Products_Features
            {
                ProductsId = product.Id,
                FeaturesId = featureIds["سازگاری"],
                Value = seed.Compatibility
            });

            if (!string.IsNullOrWhiteSpace(seed.Power))
            {
                productFeatures.Add(new _Products_Features
                {
                    ProductsId = product.Id,
                    FeaturesId = featureIds["توان"],
                    Value = seed.Power
                });
            }

            if (!string.IsNullOrWhiteSpace(seed.ConnectionType))
            {
                productFeatures.Add(new _Products_Features
                {
                    ProductsId = product.Id,
                    FeaturesId = featureIds["نوع اتصال"],
                    Value = seed.ConnectionType
                });
            }

            var colorA = colorPalette[i % colorPalette.Count];
            var colorB = colorPalette[(i + 2) % colorPalette.Count];

            productColors.Add(new _ProductsColor { ProductId = product.Id, ColorId = colorA });
            if (colorB != colorA)
            {
                productColors.Add(new _ProductsColor { ProductId = product.Id, ColorId = colorB });
            }

            if (i < 5)
            {
                comments.Add(new _Products_comment
                {
                    ProductsId = product.Id,
                    Name = "کاربر تست",
                    Email = "qa@mahan.test",
                    Comment = $"نظر تستی برای {seed.Title}",
                    CreateTime = DateTime.UtcNow.AddMinutes(-i),
                    ParentId = null
                });
            }
        }

        await db.Products_Tags.AddRangeAsync(tags, cancellationToken);
        await db.Products_Galleries.AddRangeAsync(galleries, cancellationToken);
        await db.Products_Features.AddRangeAsync(productFeatures, cancellationToken);
        await db.productsColors.AddRangeAsync(productColors, cancellationToken);
        await db.Products_Comments.AddRangeAsync(comments, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        var firstProduct = seededProducts.FirstOrDefault();
        var firstComment = await db.Products_Comments
            .Where(c => firstProduct != null && c.ProductsId == firstProduct.Id && c.ParentId == null)
            .OrderBy(c => c.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (firstProduct != null && firstComment != null)
        {
            db.Products_Comments.Add(new _Products_comment
            {
                ProductsId = firstProduct.Id,
                Name = "پاسخ پشتیبانی",
                Email = "support@mahan.test",
                Comment = "پاسخ تستی جهت پوشش کامنت تو در تو.",
                CreateTime = DateTime.UtcNow,
                ParentId = firstComment.Id
            });
            await db.SaveChangesAsync(cancellationToken);
        }

        await tx.CommitAsync(cancellationToken);

        var count = await db.Products.CountAsync(p => p.Title.StartsWith(ProductPrefix), cancellationToken);
        logger.LogInformation("Workflow preview seed completed. Seeded products: {Count}", count);
    }

    private static async Task ClearPreviousWorkflowSeedAsync(MyDbContext db, CancellationToken cancellationToken)
    {
        var workflowProductIds = await db.Products
            .Where(p => p.Title.StartsWith(ProductPrefix))
            .Select(p => p.Id)
            .ToListAsync(cancellationToken);

        if (workflowProductIds.Count == 0)
        {
            return;
        }

        var productIdSet = workflowProductIds.ToHashSet();

        db.productsColors.RemoveRange(await db.productsColors
            .Where(pc => productIdSet.Contains(pc.ProductId))
            .ToListAsync(cancellationToken));

        db.Products_Features.RemoveRange(await db.Products_Features
            .Where(pf => productIdSet.Contains(pf.ProductsId))
            .ToListAsync(cancellationToken));

        db.Products_Galleries.RemoveRange(await db.Products_Galleries
            .Where(pg => productIdSet.Contains(pg.ProductsId))
            .ToListAsync(cancellationToken));

        db.Products_Tags.RemoveRange(await db.Products_Tags
            .Where(pt => productIdSet.Contains(pt.ProductsId))
            .ToListAsync(cancellationToken));

        var usersWithProductRef = await db.Users
            .Where(u => u.ProductsId.HasValue && productIdSet.Contains(u.ProductsId.Value))
            .ToListAsync(cancellationToken);
        foreach (var user in usersWithProductRef)
        {
            user.ProductsId = null;
        }

        db.CodePostals.RemoveRange(await db.CodePostals
            .Where(cp => productIdSet.Contains(cp.ProductsId))
            .ToListAsync(cancellationToken));

        db.Products_Comments.RemoveRange(await db.Products_Comments
            .Where(c => productIdSet.Contains(c.ProductsId) && c.ParentId.HasValue)
            .ToListAsync(cancellationToken));
        await db.SaveChangesAsync(cancellationToken);

        db.Products_Comments.RemoveRange(await db.Products_Comments
            .Where(c => productIdSet.Contains(c.ProductsId))
            .ToListAsync(cancellationToken));

        db.Products.RemoveRange(await db.Products
            .Where(p => productIdSet.Contains(p.Id))
            .ToListAsync(cancellationToken));

        await db.SaveChangesAsync(cancellationToken);
    }

    private static async Task<Dictionary<string, int>> EnsureGroupsAsync(MyDbContext db, CancellationToken cancellationToken)
    {
        var titles = new[]
        {
            "قاب و کاور",
            "شارژ و کابل",
            "گجت",
            "گلس و محافظ"
        };

        var existing = await db.Products_Groups
            .Where(g => titles.Contains(g.GroupTitle))
            .ToDictionaryAsync(g => g.GroupTitle, g => g.Id, cancellationToken);

        foreach (var title in titles)
        {
            if (existing.ContainsKey(title))
            {
                continue;
            }

            db.Products_Groups.Add(new _Products_Groups { GroupTitle = title });
        }

        await db.SaveChangesAsync(cancellationToken);

        return await db.Products_Groups
            .Where(g => titles.Contains(g.GroupTitle))
            .ToDictionaryAsync(g => g.GroupTitle, g => g.Id, cancellationToken);
    }

    private static async Task<Dictionary<string, int>> EnsureSubmenusAsync(
        MyDbContext db,
        IReadOnlyDictionary<string, int> groupIds,
        CancellationToken cancellationToken)
    {
        var submenuDefs = new (string GroupTitle, string SubmenuTitle)[]
        {
            ("قاب و کاور", "قاب سیلیکونی"),
            ("قاب و کاور", "قاب شفاف"),
            ("شارژ و کابل", "شارژر دیواری"),
            ("شارژ و کابل", "کابل شارژ"),
            ("گجت", "هولدر و پایه"),
            ("گجت", "هدفون و هندزفری"),
            ("گلس و محافظ", "گلس صفحه"),
            ("گلس و محافظ", "محافظ لنز")
        };

        var groupIdSet = groupIds.Values.ToHashSet();
        var existingSubmenus = await db.submenuGroups
            .Where(s => groupIdSet.Contains(s.Products_GroupsId))
            .ToListAsync(cancellationToken);

        foreach (var def in submenuDefs)
        {
            var groupId = groupIds[def.GroupTitle];
            var exists = existingSubmenus.Any(s =>
                s.Products_GroupsId == groupId && s.Title == def.SubmenuTitle);

            if (!exists)
            {
                db.submenuGroups.Add(new _SubmenuGroups
                {
                    Products_GroupsId = groupId,
                    Title = def.SubmenuTitle
                });
            }
        }

        await db.SaveChangesAsync(cancellationToken);

        var refreshed = await db.submenuGroups
            .Where(s => groupIdSet.Contains(s.Products_GroupsId))
            .ToListAsync(cancellationToken);

        var map = new Dictionary<string, int>(StringComparer.Ordinal);
        foreach (var submenu in refreshed)
        {
            var groupTitle = groupIds.First(g => g.Value == submenu.Products_GroupsId).Key;
            map[BuildSubmenuKey(groupTitle, submenu.Title)] = submenu.Id;
        }

        return map;
    }

    private static async Task<Dictionary<string, int>> EnsureFeaturesAsync(MyDbContext db, CancellationToken cancellationToken)
    {
        var titles = new[]
        {
            "برند",
            "گارانتی",
            "جنس",
            "سازگاری",
            "توان",
            "نوع اتصال"
        };

        var existing = await db.Features
            .Where(f => titles.Contains(f.FeaturesTitle))
            .ToDictionaryAsync(f => f.FeaturesTitle, f => f.Id, cancellationToken);

        foreach (var title in titles)
        {
            if (!existing.ContainsKey(title))
            {
                db.Features.Add(new _Features { FeaturesTitle = title });
            }
        }

        await db.SaveChangesAsync(cancellationToken);

        return await db.Features
            .Where(f => titles.Contains(f.FeaturesTitle))
            .ToDictionaryAsync(f => f.FeaturesTitle, f => f.Id, cancellationToken);
    }

    private static async Task<Dictionary<string, int>> EnsureColorsAsync(MyDbContext db, CancellationToken cancellationToken)
    {
        var names = new[] { "مشکی", "سفید", "آبی", "قرمز", "سبز" };

        var existing = await db.colors
            .Where(c => names.Contains(c.Name))
            .ToDictionaryAsync(c => c.Name, c => c.Id, cancellationToken);

        foreach (var name in names)
        {
            if (!existing.ContainsKey(name))
            {
                db.colors.Add(new _Color { Name = name });
            }
        }

        await db.SaveChangesAsync(cancellationToken);

        return await db.colors
            .Where(c => names.Contains(c.Name))
            .ToDictionaryAsync(c => c.Name, c => c.Id, cancellationToken);
    }

    private static string BuildSubmenuKey(string groupTitle, string submenuTitle)
        => $"{groupTitle}::{submenuTitle}";

    private static List<ProductSeed> BuildProductSeeds() =>
        new()
        {
            new("قاب و کاور", "قاب سیلیکونی", "قاب سیلیکونی آیفون 15", "Apple", "قاب سبک با جذب ضربه مناسب استفاده روزمره.", 690000, 15, true, "۱۸ ماه", "سیلیکون", "iPhone 15", null, null),
            new("قاب و کاور", "قاب شفاف", "قاب شفاف سامسونگ S24", "Samsung", "قاب شفاف ضد زردی با لبه مقاوم در برابر ضربه.", 580000, 22, false, "۱۲ ماه", "پلی‌کربنات", "Galaxy S24", null, null),
            new("شارژ و کابل", "شارژر دیواری", "شارژر دیواری 33 وات", "Xiaomi", "شارژر سریع مناسب گوشی و تبلت با ایمنی چندگانه.", 980000, 9, true, "۱۸ ماه", "ABS فشرده", "Android / iOS", "33W", "USB-C"),
            new("شارژ و کابل", "کابل شارژ", "کابل تایپ‌سی به تایپ‌سی", "Anker", "کابل مقاوم با روکش تقویت‌شده و انتقال پایدار.", 420000, 30, false, "۱۲ ماه", "نایلون بافته", "USB-C Devices", null, "Type-C"),
            new("گجت", "هولدر و پایه", "هولدر مگنتی دریچه کولر", "Baseus", "هولدر جمع‌وجور با مگنت قوی و نصب سریع.", 760000, 12, true, "۱۲ ماه", "آلیاژ + سیلیکون", "همه موبایل‌ها", null, null),
            new("گجت", "هدفون و هندزفری", "هندزفری بلوتوث اقتصادی", "QCY", "هندزفری با تاخیر کم برای تماس و موسیقی.", 1450000, 18, true, "۱۸ ماه", "پلاستیک فشرده", "Android / iOS", null, "Bluetooth"),
            new("گلس و محافظ", "گلس صفحه", "گلس پرایوسی آیفون 14", "Nillkin", "محافظ صفحه با کاهش دید جانبی و لمس روان.", 510000, 20, false, "۱۲ ماه", "شیشه حرارت‌دیده", "iPhone 14", null, null),
            new("گلس و محافظ", "محافظ لنز", "محافظ لنز سامسونگ A55", "Lito", "محافظ لنز ضدخش با نصب آسان و شفافیت بالا.", 350000, 26, false, "۶ ماه", "شیشه نانو", "Galaxy A55", null, null),
            new("شارژ و کابل", "شارژر دیواری", "شارژر دیواری 65 وات GaN", "UGREEN", "شارژر پرقدرت برای لپ‌تاپ و موبایل با ابعاد کوچک.", 1890000, 7, true, "۱۸ ماه", "GaN", "Laptop / Mobile", "65W", "USB-C + USB-A"),
            new("قاب و کاور", "قاب سیلیکونی", "قاب سیلیکونی شیائومی 13", "Xiaomi", "قاب نرم ضدلغزش مناسب استفاده طولانی.", 620000, 0, false, "۱۲ ماه", "سیلیکون مات", "Xiaomi 13", null, null)
        };

    private sealed record ProductSeed(
        string GroupTitle,
        string SubmenuTitle,
        string Title,
        string Brand,
        string DescriptionText,
        int Price,
        int Count,
        bool Recommended,
        string Warranty,
        string Material,
        string Compatibility,
        string? Power,
        string? ConnectionType);
}
