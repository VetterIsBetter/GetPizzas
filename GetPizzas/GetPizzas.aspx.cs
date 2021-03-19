using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using PizzaModel;

namespace GetPizzas
{
    public partial class GetPizzas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        public List<Pizza> GetPizzaCombos()
        {
            try
            {
                List<Pizza> pizzas;
                string url = "https://www.brightway.com/CodeTests/pizzas.json";
                HttpWebRequest httpWebRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
                
                using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
                {                   
                    Stream stream = httpWebResponse.GetResponseStream();
                    string json = new StreamReader(stream).ReadToEnd();
                    stream.Close();
                    
                    pizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);                   
                }
                return pizzas;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        // Aggregates all topping combinations from list of pizzas
        // Returns each combination and how many times that combo was ordered
        public IEnumerable<PizzaConfigModel> GetToppingCombinations(List<Pizza> pizzas)
        {
            // Order each pizza's toppings in descending order (alphabetically)
            var orderedPizzaToppings = pizzas.Select(x => x.toppings.OrderBy(topping => topping));

            // Takes the json array of toppings and aggregates them together into one string per pizza
            var aggregatedToppingsList = orderedPizzaToppings.Select((toppings => toppings.Aggregate((a,b) => a + "," + b)));

            // Group toppings and how many times they were ordered in that configuration
            var groupedToppings = aggregatedToppingsList.GroupBy(toppings => toppings).Select(x => new PizzaConfigModel() { toppingsOrdered = x.Key, timesOrdered = x.Count() });

            return groupedToppings;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {      
            List<Pizza> pizzas = GetPizzaCombos();
            if (pizzas == null) return;

            IEnumerable<PizzaConfigModel> allToppingCombinations = GetToppingCombinations(pizzas);

            // top 20 most ordered combos
            IEnumerable<PizzaConfigModel> top20Combos = allToppingCombinations.OrderByDescending(ag => ag.timesOrdered).Take(20);

            foreach (PizzaConfigModel combo in top20Combos)
            {
                //Response.Write("<li>" + "Pizza with toppings: " + combo.toppingsOrdered + " ordered a total of " + combo.timesOrdered + " times." + "</li></br>");
                var htmlString = "Pizza with toppings: " + combo.toppingsOrdered + " ordered a total of " + combo.timesOrdered + " times.</br>";
                HtmlGenericControl htmlGenericControl = new HtmlGenericControl("li");
                HtmlGenericControl li = htmlGenericControl;
                PizzasOrdered.Controls.Add(li);           
                li.InnerHtml = htmlString;
            }
        }
    }
}