using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTOs
{
    public class ConsultaDTO
    {
        public ContentsDTO[] contents {  get; set; } 
    }

    public class ContentsDTO
    {
        public PartsDTO[] parts { get; set; }
    }

    public class PartsDTO
    {
        public string text { get; set; }
    }

}
