using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintReceiptDemo
{
    //Création de la classe Receipt
    public class Receipt
    {
        public int Id {get; set;}
        public string NomArticle { get; set; }
        public double Prix { get; set; }
        public int Quantite { get; set; }
        public string Total { get { return string.Format($"{Quantite * Prix}$"); } }
    }
}
