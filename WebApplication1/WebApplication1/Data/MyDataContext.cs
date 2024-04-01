using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class MyDataContext
    {
        public List<Field> Fields { get; set; }
        public List<Planepath> Planepaths { get; set; }

        public MyDataContext() 
        {
            Fields = new List<Field>();
            Planepaths = new List<Planepath>();
        }  
    }
}
