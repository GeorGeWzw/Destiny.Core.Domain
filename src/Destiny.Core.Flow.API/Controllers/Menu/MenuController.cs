﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Destiny.Core.Flow.AspNetCore.Ui;
using Destiny.Core.Flow.Dtos.Menu;
using Destiny.Core.Flow.Filter;
using Destiny.Core.Flow.IServices.IMenu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Destiny.Core.Flow.API.Controllers.Menu
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuServices _menuServices;

        public MenuController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }

        /// <summary>
        /// 获取树形菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAsync")]
        [Description("获取树形菜单信息")]

        public async Task<TreeData<MenuOutDto>> GetAsync()
        {
            var result = await _menuServices.GetMenuAsync();
            return new TreeData<MenuOutDto>()
            {
                Data = result.Data,
                Message = result.Message,
                Success = result.Success
            };
        }
        /// <summary>
        /// 获取表格菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("获取表格菜单信息")]
        [Route("GetTableAsync")]
        public async Task<PageList<MenuTableOutDto>> GetTableAsync()
        {
            return (await _menuServices.GetMenuTableAsync(new PageRequest() { })).PageList();
        }


        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("添加或修改")]
        [Route("AddMenuAsync")]
        public async Task<AjaxResult> AddMenuAsync([FromBody]MenuInputDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                return (await _menuServices.CareateAsync(dto)).ToAjaxResult();
            }
            return (await _menuServices.UpdateAsync(dto)).ToAjaxResult();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("删除菜单")]
        [Route("Delete")]
        public async Task<AjaxResult> Delete(Guid? id)
        {
            return (await _menuServices.DeleteAsync(id.Value)).ToAjaxResult();
        }
    }
}