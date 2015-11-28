//******************************************************
//Common Javascript Objects
//******************************************************
//Description:
//These are common javascript objects to be used in any given project
//******************************************************
//Update:
//11/15/2014    Zack Lore   Created Objects
//******************************************************

//borrowed code from stackOverflow for formatting str.  Thank you Patrick Desjardins.
//use like... (123456789.12345).formatDecimal(2, '.', ',');
Number.prototype.formatDecimal = function(c, d, t){
var n = this, c = isNaN(c = Math.abs(c)) ? 2 : c, d = d === undefined ? "," : d, t = t == undefined ? "." : t, s = n < 0 ? "-" : "", i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "", j = (j = i.length) > 3 ? j % 3 : 0;
   return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
 };

 function stripFormat(str)
{
    if(str == null || str == "") { return ""; }
    var rx_formatting = /[$,%\s\(\)lbs]/g;
    var clean = str.replace(rx_formatting, "");
    if(clean === "-")
    {
        clean = 0.0;
    }

    //Math.round(number * 1000) / 1000 is to adjust javascript math accuracy
    return Math.round(clean * 1000) / 1000;
}


///A page object.  This holds all associated information for a given object
///inputRef = the object on the page that takes input or output from object (text, dropdown, etc)
///inputType = the type of control (text, select, etc)
///inputID = id of inputRef object
///inputName = name of inputRef object
///defaultVal = default value if any
///valType = type of value (str, int, dbl, currency, percent)
///outputVal = formatted output value
///val = the value of the object;
function Obj(id)
{
    this.inputRef       = null;
    this.inputType      = null;
    this.inputId        = null;
    this.inputName      = "";
    this.defaultVal     = "";
  	this.defaultColor	= "#B0B0B0";
  	this.activeColor	= "#000";
    this.valType        = null;
    this.outputVal      = null;
    this.val            = null;

    this.dblDecimal     = 2;
    this.percentDecimal = 2;

    this.itemList       = null;

    this.find = function(str) {
        this.inputRef = window.document.getElementById(str);

        if(this.inputRef !== null){
            this.inputId = this.inputRef.id;
            this.inputRef.style.textAlign = "right";
            this.inputType = this.inputRef.type;
            this.inputName = this.inputRef.name;
			var _ref = this;
			this.inputRef.addEventListener("change", function() { _ref.updateVal(); } );
        } else {
            var error = new Error();
            alert("find() failed for " + this.toString() + "\nerror:" + error.stack);
        }
    };

	this.setDefaultColor = function(str){
		this.defaultColor = str;
		this.refreshColor();
	};

  this.setDecimalPlace = function(type, num) {
        if(type !== "")
        {
            if(type === "dbl")
            {
                if(num !== "" && !isNaN(num))
                {
                    this.dblDecimal = parseInt(num);
                }
            }
            else if(type === "percent")
            {
                if(num !== "" && !isNaN(num))
                {
                    this.percentDecimal = parseInt(num);
                }
            }
        }
    };

	this.refreshColor = function(){
		this.inputRef.style.color = (this.val === this.defaultVal) ? this.defaultColor : this.activeColor;
	}

  this.setInputRef = function(obj)
  {
        this.inputRef = obj;
    };

  //@list: array of objects [text, value] format
  //@updateObj: boolean indicating if new list should be applied to object
  this.setItemList = function(list, updateObj){
    if(this.inputRef.type != "select-one") { return; }
    if(list != null)
    {
      if(list.length > 0)
      {
        this.itemList = list;

        if(updateObj === true)
        {
          //clear dropdown
          while(this.inputRef.options.length > 0)
          {
            this.inputRef.remove(0);
          }

          var l = this.itemList.length;
          for(var i=0; i<l; i++)
          {
            var c = this.itemList[i];
            var opt = document.createElement("option");
            opt.text = c.text;
            opt.value = c.value;

            this.inputRef.add(opt);
          }
        }
      }
    }
  };

  this.setDefaultVal = function(_val) {
      if(_val == null) { console.log("default value = null"); return; }
      this.defaultVal = _val;
      this.updateOutputVal();
  };

    this.setReadOnly = function(_bool) {
        if(_bool == null) { console.log("Read Only bool = null."); return; }
        if(this.inputRef == null) { console.log("Input Ref = null"); return; }
        this.inputRef.readOnly = _bool;
    };

    this.setVal = function(_val) {
        if(_val == null) { console.log("_val = null"); return; }
        this.val = _val;
        this.updateOutputVal();
    };

    this.updateOutputVal = function() {
        if(this.val !== null){
            if(this.val === ""){
                if(this.defaultVal)
                {
                    this.val = this.defaultVal;
                    this.updateOutputVal();
                }
                return;
            }

            if(this.valType !== null)
            {
                switch(this.valType)
                {
                    case "int"      : this.outputVal = parseFloat(this.val).formatDecimal(0, ".", ",");
                        break;
                    case "dbl"      : this.outputVal = parseFloat(this.val).formatDecimal(this.dblDecimal, ".", ",");
                        break;
                    case "currency" : this.outputVal = "$" + parseFloat(this.val).formatDecimal(2, ".", ",");
                        break;
                    case "percent"  : this.outputVal = parseFloat(this.val).formatDecimal(this.percentDecimal, ".", ",") + "%";
                        break;
                    default         : this.outputVal = this.val;
                                      this.inputRef.style.textAlign = "left";
                        break;
                }

				this.refreshColor();

                if(this.inputType === "text"){
                    this.inputRef.value = this.outputVal;
                } else if ( this.inputType === "select-one" ) {
                    for(var i=0; i<this.inputRef.options.length; i++)
                    {
                        if(this.outputVal == this.inputRef.options[i].text){
                            this.inputRef.selectedIndex = i;
                        }
                    }
                } else {
                    this.inputRef.innerHTML = this.outputVal;
                }
            }
        } else {
            if(this.defaultVal !== null)
            {
                this.val = this.defaultVal;
                this.updateOutputVal();
            }
        }
    };

    this.updateVal = function() {
        var temp = this.inputRef.value;
		if(this.valType !== null)
		{
			switch(this.valType)
			{
				case "int"      : this.val = !isNaN(stripFormat(temp)) ? parseInt(stripFormat(temp)) : 0;
					break;
				case "dbl"      : this.val = !isNaN(stripFormat(temp)) ? parseFloat(stripFormat(temp)) : 0.0;
					break;
				case "currency" : this.val = !isNaN(stripFormat(temp)) ? parseFloat(stripFormat(temp)) : 0.0;
					break;
				case "percent"  : this.val = !isNaN(stripFormat(temp)) ? parseFloat(stripFormat(temp)) : 0.0;
					break;
				default         : this.val = temp;
					break;
			}
			if(this.val === "" || this.val === 0.0)
			{
				this.val = this.defaultVal;
			}
		}
		else
		{
			this.val = null;
		}
		this.updateOutputVal();
    };

    if(id !== null && id !== undefined){
        this.find(id);
        if(this.inputRef === null)
        {
            alert("Could not set " + id);
        }
    }
}

///Override toString Method
Obj.prototype.toString = function()
                            {
                                var str = [];
                                for (var key in this) {
                                    if (this.hasOwnProperty(key) && typeof this[key] !== "function") str.push(key + ':' + this[key]);
                                }
                                return '*Obj*\n' + str.join("\n");
                            };
