var Product = function() {

var  = new Obj('txt_Product_');
.valType = '';
.setDefaultVal('');
.setDefaultColor('');

var  = new Obj('txt_Product_');
.valType = '';
.setDefaultVal('');
.setDefaultColor('');

var  = new Obj('txt_Product_');
.valType = '';
.setDefaultVal('');
.setDefaultColor('');

var Name = new Obj('txt_Product_Name');
Name.valType = 'str';
Name.setDefaultVal('Product Name');
Name.setDefaultColor('#999999');

var Price = new Obj('txt_Product_Price');
Price.valType = 'currency';
Price.setDefaultVal('0');
Price.setDefaultColor('#999999');

var Count = new Obj('txt_Product_Count');
Count.valType = 'int';
Count.setDefaultVal('0');
Count.setDefaultColor('#999999');

var collectValues = function() {
var returnObject = {
 : .val ,
 : .val ,
 : .val ,
Name : Name.val ,
Price : Price.val ,
Count : Count.val
};

return returnObject;

};

}

