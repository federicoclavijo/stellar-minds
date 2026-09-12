using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.DTOs
{
    public class RespuestaEvaluacionDTO
    {
        
        public List<CandidateDTO> candidates { get; set; }
        public class CandidateDTO
        {
            public ContentDTO content { get; set; }
        }

        public class ContentDTO
        {
            public List<PartDTO> parts { get; set; }
        }

        public class PartDTO
        {
            public string text { get; set; }
        }
        }

}
