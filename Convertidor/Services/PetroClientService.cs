
using Convertidor.Data.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Convertidor.Services
{

public class PetroClientService : IPetroClientService
    {

        private readonly HttpClient _client;
       

        public PetroClientService(HttpClient httpClient)
        {

          

        
             //httpClient.BaseAddress = new Uri("https://petroapp-price.petro.gob.ve/price/PTR");
            httpClient.BaseAddress = new Uri("https://petroapp-price.petro.gob.ve/price/");



            _client = httpClient;

        }

        public async Task<PetroBsGetDto> GetPetroFiat()
        {

            BodyPetroDto body = new BodyPetroDto();






            try
            {


                string coin = "PTR";
                string fiat = "BS";

                var coins = new List<string>();
                var fiats = new List<string>();
                coins.Add(coin);
                fiats.Add(fiat);
                body.fiats=fiats;
                body.coins = coins;



                string json1 = JsonConvert.SerializeObject(body);
                StringContent data = new StringContent(json1, Encoding.UTF8, "application/json");



                PetroBsGetDto result = await Post(data);

                return result;


            }
            catch (Exception e)
            {
                var msg = e.InnerException.Message;

                return null;
            }





        }


        public async Task GetPetro()
        {

            BodyPetroDto body = new BodyPetroDto();






            try
            {


                string coin = "BTC";
                string fiat = "PTR";
              
                var coins = new List<string>();
                var fiats = new List<string>();
                coins.Add(coin);
                fiats.Add(fiat);
                //body.fiats=fiats;
                body.coins = coins;
                       


                string json1 = JsonConvert.SerializeObject(body);
               StringContent data = new StringContent(json1, Encoding.UTF8, "application/json");



                var result = await Post(data);
               



            }
            catch (Exception e)
            {
                var msg = e.InnerException.Message;

                return;
            }





        }


        public async Task<PetroBsGetDto> Post(StringContent data)
        {
            ResultDto<string> metadata = new ResultDto<string>("");

            try
            {
                _client.DefaultRequestHeaders.Clear();
              
                _client.DefaultRequestHeaders.Add("Accept", "application/json");
                //_client.DefaultRequestHeaders.Add("x-csrf-token", token);
                //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("x-csrf-token", token);



                var result = await _client.PostAsync(_client.BaseAddress, data);
                string resultContent = await result.Content.ReadAsStringAsync();

                var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<PetroBsGetDto>(resultContent);
             
                metadata.IsValid = result.IsSuccessStatusCode;
                metadata.Message = resultContent;
                metadata.Data = resultContent;


                return respuesta;
            }
            catch (Exception ex)
            {

                metadata.IsValid = false;
                metadata.Message = ex.InnerException.Message;
                return null;
            }

            // return await _client.GetStringAsync("/");
        }



        public async Task<string> GetData()
        {

            var result = await _client.GetAsync("/");
            return await _client.GetStringAsync("/");
        }

    }
}
