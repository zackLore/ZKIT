var Grotto = function() {

var Temk = new Obj('txt_Grotto_Temk');
Temk.valType = 'str';
Temk.setDefaultVal('');
Temk.setDefaultColor('');

var Corenc = new Obj('txt_Grotto_Corenc');
Corenc.valType = 'currency';
Corenc.setDefaultVal('0');
Corenc.setDefaultColor('0');

var collectValues = function() {
var returnObject = {
Temk : Temk.val ,
Corenc : Corenc.val
};

return returnObject;

};

}

