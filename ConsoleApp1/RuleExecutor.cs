using RuleEngine;
using RuleEngine.Compiler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ConsoleApp1
{
  

internal sealed class RuleExecutor
    {


            

    public void ExecuteTaxRules()
        {
            XmlDocument rules = new XmlDocument();

            string directory = AppDomain.CurrentDomain.BaseDirectory + @"\ACA.xml";
            string inBond, inFavorite, inCity ;
            bool isNumeric = true;
            int n;
       
            rules.Load(directory);
            //model
            XmlDocument model = new XmlDocument();
            model.LoadXml(@"<Courier><Bond>500000</Bond><City>Taguig</City><Favorite>1</Favorite><CourierID></CourierID></Courier>");

            ROM rom = Compiler.Compile(rules);
            rom.AddModel("Courier", model);
           // rom.Evaluate();
            var CID = model["Courier"]["CourierID"].InnerText;
            
            var message = string.Format("Assigned to courier id {0}", CID);
           // Console.WriteLine(message);
            do
            {
                do {
                    if (isNumeric == false)
                        Console.Write("Input is invalid; re-input bond:  ");
                    else
                        Console.Write("Input bond:  ");
                    inBond = Console.ReadLine();
                    isNumeric = int.TryParse(inBond, out n);
                } while (isNumeric != true);

                isNumeric = true;

                do {
                    if (isNumeric == false)
                        Console.Write("Input is invalid; re-input Favorite:  ");
                    else
                        Console.Write("Input Favorite:  ");
                    inFavorite = Console.ReadLine();
                    isNumeric = int.TryParse(inFavorite, out n);
                } while (isNumeric != true);

                Console.Write("Input City:  ");
                inCity = Console.ReadLine();
                if (inCity == "x") break;



             model["Courier"]["Bond"].InnerText = inBond;
             model["Courier"]["Favorite"].InnerText = inFavorite;
             model["Courier"]["City"].InnerText = inCity;
                rom.Evaluate();
             CID = model["Courier"]["CourierID"].InnerText;
             message = string.Format("Assigned to courier id {0}", CID);
             Console.WriteLine(message);
              
                

            } while (inCity != "X");
           

        }


    }
}
