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
    var grotto = Grotto.collectValues();
    requests.push($.ajax({
        url: save_Kleeto.php,
        type: 'post',
        data: JSON.stringify(grotto) });    );
    if(requests.length > 0)
    {
                initiateRequest();
    }
}
