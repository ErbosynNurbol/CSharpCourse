



// Converter converter = new Cyrl2Latyn();

// string latynText =  converter.Convert("Cyrl Text");

IDataHelper dataHelper = new OracleHelper();
var list  = dataHelper.GetList(10);