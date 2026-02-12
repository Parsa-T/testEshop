using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Services
{
    public class SubGroupsServices : ISubGroupsServices
    {
        IProductsGroupeRepository _productsGroupeRepository;
        ISubMenuGroupsRepository _subMenuGroupsRepository;
        public SubGroupsServices(IProductsGroupeRepository productsGroupe, ISubMenuGroupsRepository subMenuGroupsRepository)
        {
            _productsGroupeRepository = productsGroupe;
            _subMenuGroupsRepository = subMenuGroupsRepository;
        }

        public async Task AddSubMenuAsync(_SubmenuGroups submenuGroups)
        {
            await _subMenuGroupsRepository.AddSubMenu(submenuGroups);
        }

        public async Task<SubMenuGroupsDTO?> GetSubGroupsForId(int id)
        {
            var subGroups = await _subMenuGroupsRepository.FindForIdSubMenu(id);
            if(subGroups==null)
                return null;
            var groups = await _productsGroupeRepository.GetAllGroups();
            return new SubMenuGroupsDTO
            {
                Id = subGroups.Id,
                Products_GroupsId = subGroups.Products_GroupsId,
                Title = subGroups.Title,
                ShowAllGroups = groups.Select(g => new AddOrEditGroupsDTO
                {
                    GroupTitle = g.GroupTitle,
                    Id = g.Id,
                }).ToList()
            };
        }

        public async Task<SubMenuGroupsDTO> ShowAllSubGroups()
        {
            var groups = await _productsGroupeRepository.GetAllGroups();
            var list = groups.Select(g=>new AddOrEditGroupsDTO
            {
                GroupTitle = g.GroupTitle,
                Id = g.Id,
            }).ToList();
            return new SubMenuGroupsDTO
            {
                ShowAllGroups = list,
            };
        }

        public async Task<IEnumerable<_Products>> ShowSubMenuById(int id)
        {
            return await _subMenuGroupsRepository.GetSubMenuById(id);
        }

        public async Task<IEnumerable<_SubmenuGroups>> ShowSubMenuGroups()
        {
           return await _subMenuGroupsRepository.GetAllSubGroups();
        }

        public async Task UpdateSubMenuAsync(SubMenuGroupsDTO dTO)
        {
            var subMenu = await _subMenuGroupsRepository.FindForIdSubMenu(dTO.Id);
            if (subMenu == null)
                return;
            subMenu.Title = dTO.Title;
            subMenu.Products_GroupsId = dTO.Products_GroupsId;
            await _subMenuGroupsRepository.UpdateSubMenu(subMenu);
        }
    }
}
