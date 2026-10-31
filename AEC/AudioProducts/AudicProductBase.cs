using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.AudioProducts
{
    public class AudicProductBase
    {
        public string brand;            //品牌
        public string model;            //型号
        
        public AudicProductBase(string brand_, string model_)
        {
            this.brand = brand_;
            this.model = model_;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!typeof(object).IsInstanceOfType(this.GetType()))
            {
                return false;
            }

            AudicProductBase other  = obj as AudicProductBase;

            return String.Equals(this.brand, other.brand, StringComparison.CurrentCultureIgnoreCase) &&
                String.Equals(this.model, other.model, StringComparison.CurrentCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return (this.brand + this.model).GetHashCode();
        }

        public virtual string basicInfo
        {
            get
            {
                return string.Format("{0}  {1}", this.brand, this.model);
            }
        }
    }
}
