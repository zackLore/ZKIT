var request;
var requests = [];
function initiateRequest()
{
    if(requests.length > 0)
    {
        request = requests.pop();
            console.log(request);
            request.done = function(response, textStatus, jqXHR) {
                if(requests.length > 0)
                {
                    initiateRequest();
                }
            };
            request.fail = function(jqXHR, textStatus, errorThrown) {
                alert('An error has occured while attempting to save: ' + errorThrown);
            };
        }
    }
function save()
{
        var person = PersonObj.collectValues();
        requests.push($.ajax({
            url: 'PHP/save_Project.php',
            type: 'post',
            data: {Person : JSON.stringify(person)} })        
);
            if(requests.length > 0)
        {
                        initiateRequest();
        }
        else
            {
                alert('No Requests Sent.');
        }
    }
function loadData()
{
    var d = document.getElementById('content');
        request = $.ajax({
            url: 'PHP/load_Project.php',
            type: 'post',
            datatype: 'json',
            success: function(responseText, textStatus, jqXHR)
                {
                    var objects = JSON.parse(responseText);
                    for (var i = 0; i < objects.length; i++)
                        {
                            var o = objects[i];
                            d.innerHTML += 'ProductID: ' + o.ProductId + ' Name: ' + o.Name + ' Price: ' + o.Price + ' < br /> ';
                        }
                    }, 
                    fail: function(jqXHR, textStatus, errorThrown)
                        {
                            alert('An error has occured while attempting to save: ' + errorThrown);
                        } 
                    } );
                }
