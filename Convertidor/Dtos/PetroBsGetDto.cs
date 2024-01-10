namespace Convertidor.Dtos
{
	public class PetroBsGetDto
	{
        public class BS
        {
        }

        public class Data
        {
            public PTR PTR { get; set; }
            public ExtraData extraData { get; set; }
        }

        public class ExtraData
        {
            public PTR PTR { get; set; }
        }

        public class PTR
        {
            public double BS { get; set; }
        }

        public class Root
        {
            public int status { get; set; }
            public bool success { get; set; }
            public Data data { get; set; }
            public string message { get; set; }
        }

    }
}

