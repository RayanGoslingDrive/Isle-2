using System.Linq;
using System.Reflection;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services
{
    public class Service : IService
    {
        private MyDataContext _dataContext;
        public Service(MyDataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        private void CalculatePlanePath(Field model, Planepath planepath)
        {
            if (model.Map.Length != model.N)
            {
                Console.WriteLine("Incorrect input");
                return;
            }
            foreach (var line in model.Map) 
            {
                if (line.Length != model.M)
                {
                    Console.WriteLine("Incorrect input");
                    return;
                }
                for (int i = 0; i < line.Length; i++) 
                {
                    if (line[i] != '.' && line[i] != '#')
                    {
                        Console.WriteLine("Incorrect input");
                        return;
                    }
                }

            }

            _dataContext.Fields.Add(model);
            List<string> strings = new List<string>();
            string workaroundstring = "";
            string[] newMap = new string[model.N + 2];
            for (int i = 0; i < model.Map.Count(); i++)
            {
                strings.Add(model.Map[i]);
            }
            for (int i = 0; i < model.M + 2; i++)
            {
                workaroundstring += ".";
            }
            newMap[0] = workaroundstring;
            for (int i = 0; i < strings.Count(); i++)
            {
                newMap[i + 1] = "." + strings[i] + ".";
            }
            for (int i = 0; i < model.M + 2; i++)
            {
                newMap[model.N + 1] += ".";
            }

            model.Map = newMap;

            List<Coordinates> coords = new List<Coordinates>();
            planepath.coordinates = new List<Coordinates>();
            for (int i = 1; i < model.N + 1; i++)
            {
                for (int j = 1; j < model.M + 1; j++)
                {
                    if (model.Map[i][j] == '.' && (model.Map[i - 1][j] == '#' || model.Map[i + 1][j] == '#' || model.Map[i][j - 1] == '#' || model.Map[i][j + 1] == '#' || model.Map[i - 1][j - 1] == '#' || model.Map[i - 1][j + 1] == '#' || model.Map[i + 1][j - 1] == '#' || model.Map[i + 1][j + 1] == '#'))
                    {
                        coords.Add(new Coordinates { y = i, x = j });
                    }
                }
            }
            int dy, dx;
            dy = coords[0].y;
            dx = coords[0].x;
            int[] test = { 0, 1, -1 };
            bool found = false;
            while (coords.Count != 0)
            {
                planepath.coordinates.Add(new Coordinates { y = dy, x = dx });
                coords.Remove(new Coordinates { y = dy, x = dx });
                foreach (int i in test)
                {
                    foreach (int j in test)
                    {
                        if (coords.Contains(new Coordinates { y = dy + i, x = dx + j }))
                        {
                            dx += j;
                            dy += i;
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        found = false; break;
                    }
                }
            }

            
        }

        public Planepath Create(Field model)
        {
            var lastField = _dataContext.Fields.LastOrDefault();
            var lastPath = _dataContext.Planepaths.LastOrDefault();
            if (lastField != null)
            {
                model.id = lastField.id + 1;
                var planepath = new Planepath();
                planepath.id = lastPath.id + 1;
                CalculatePlanePath(model, planepath);
                _dataContext.Planepaths.Add(planepath);
                return planepath;
                
            }
            else
            {
                model.id = 1;
                var planepath = new Planepath();
                planepath.id = 1;
                CalculatePlanePath(model, planepath);
                _dataContext.Planepaths.Add(planepath);
                return planepath;
            }
            
            
        }
    }
}
