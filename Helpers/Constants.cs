using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Helpers
{
    public static class Constants
    {
        // BPCS CONNECTORS
        public static readonly string DATA_WAREHOUSE_PREFIX = "ISADB01.[DW_NSEL-CLNSA]";
        public static readonly string AS400_PREFIX = "AS400.S10CC4FB";

        // BPCS SCHEMAS
        public static readonly string STAGING_SCHEMA = "PILLX834F";
        public static readonly string PRODUCTION_SCHEMA = "ERPLXF";

        // ROUTE TYPES
        public static readonly string PRESALE = "PREVENTA";
        public static readonly string DELIVERY = "ENTREGA";
    }
}
