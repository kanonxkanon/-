using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testSelia
{

    [Serializable()]
    class Class1
    {

        String test =null;

        public Class1(){
        }

        public Class1(String str)
        {
            this.test = str;
        }


    }
}
