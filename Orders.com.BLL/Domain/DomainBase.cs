using Peasy;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
//using Newtonsoft.Json;

namespace Orders.com.BLL.Domain
{
    public abstract class DomainBase : IDomainObject<long>
    {
        // TODO: Include Newtonsoft.json??
        //[JsonIgnore, IgnoreDataMember]
        public abstract long ID { get; set; }

        public string Self { get; set; }

        [Editable(false)]
        public int? CreatedBy { get; set; }

        [Editable(false)]
        public DateTime CreatedDatetime { get; set; }

        [Editable(false)]
        public int? LastModifiedBy { get; set; }

        [Editable(false)]
        public DateTime? LastModifiedDatetime { get; set; }
    }
}
