using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlobStorage.Models
{
    public class Video
    {
        [Required]
        public string ContainerName { get; set; }

        //[Required]
        //[DisplayName ("Upload Video")]
        //public byte[] video { get; set; }
    }
}
