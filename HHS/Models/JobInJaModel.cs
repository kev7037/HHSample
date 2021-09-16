using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HHS.Models
{
    public class JobInJaModel
    {
        public JobInJaModel()
        {
        }

        public JobInJaModel(
            string title,
            string image,
            string company,
            string location,
            string contractType)
        {
            Title = title;
            Image = image;
            Company = company;
            Location = location;
            ContractType = contractType;
        }

        public string Title { get; set; }
        public string Image { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string ContractType { get; set; }
    }
}
