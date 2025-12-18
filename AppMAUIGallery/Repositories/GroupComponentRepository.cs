using AppMAUIGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMAUIGallery.Repositories {
    public partial class GroupComponentRepository : IGroupComponentRepository
    {
        private List<Component> _components;
        private List<GroupComponent> _groupComponents;
        public GroupComponentRepository()
        {
            LoadData();
        }

        public List<Component> GetComponents()
        {
            return _components;
        }

        List<GroupComponent> IGroupComponentRepository.GetGroupComponents()
        {
           return _groupComponents;
        }
    }
}
