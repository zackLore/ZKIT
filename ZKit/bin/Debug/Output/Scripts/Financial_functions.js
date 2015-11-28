var request;
var requests = [];
function initiateRequest()
{
    if(requests.length > 0)
    {
        request = requests.pop();
        request.done = function(response, textStatus, jqXHR) {
            if(requests.length > 0)
            {
                initiateRequest();
            }
        });
        request.fail = function(jqXHR, textStatus, errorThrown) {
            alert('An error has occured while attempting to save: ' + errorThrown);
        });
    }
}
function save()
{
    var product = Product.collectValues();
    requests.push($.ajax({
        url: save_Financial.php,
        type: 'post',
        data: JSON.stringify(product) });    );
    if(requests.length > 0)
    {
                initiateRequest();
    }
}
