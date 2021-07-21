﻿using Realdeal.Data;
using Realdeal.Models.Advert;
using System.Collections.Generic;
using System.Linq;

namespace Realdeal.Service.Category
{
    public class CategoryServise : ICategoryService
    {
        private readonly RealdealDbContext context;

        public CategoryServise(RealdealDbContext context)
        {
            this.context = context;
        }

        public bool DoesCategoryExist(string categoryId)
        => this.context.SubCategories.Any(c => c.Id == categoryId);

        public Dictionary<string, IEnumerable<AdvertCategoryViewModel>> GetAllCategories()
        => context.MainCategories
                .Select(s => new
                {
                    Name = s.Name,
                    SubCategories = s.SubCategories.Select(c => new AdvertCategoryViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                })
                .ToDictionary(x => x.Name, s => s.SubCategories);
    }
}
