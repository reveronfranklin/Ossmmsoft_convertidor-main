namespace Convertidor.Dtos
{
    public class ResultDto<T>
    {

        public ResultDto(T data)
        {
            Data = data;

        }

        public T? Data { get; set; }
        public bool IsValid  { get; set; }
        public string LinkData { get; set; }
        public string LinkDataArlternative { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Page { get; set; }
        public int TotalPage { get; set; }
        
        public int CantidadRegistros { get; set; }


    }


   

}
