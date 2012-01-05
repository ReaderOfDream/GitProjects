using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Repository;

namespace ManagementCompany.Models
{
    public class ManagementObjectsViewModel
    {
        public ManagementObjectsViewModel()
        {
            using (var context = new MCDatabaseModelContainer())
            {
                var key = new EntityKey();
                context.ObjectStateManager.GetObjectStateEntry(key);
                Buildings = context.Buildings.ToList();
            }
        }

        public List<Building> Buildings { get; set; }
    }
}

