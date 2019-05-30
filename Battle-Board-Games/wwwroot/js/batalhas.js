var ObterQuantidadeBatalha;
$(function (id) {
    var baseUrl = window.location.protocol + "//" +
        window.location.hostname +
        (window.location.port ? ':' + window.location.port : '');
    ObterQuantidadeBatalha = function () {
        var urlObterBatalha = baseUrl + "/api/BatalhasAPI/BatalhasSize/";
        var headers = {};
        $.ajax({
            type: 'GET',
            url: urlObterBatalha,
            headers: headers
        })
            .done(function (data) {
                document.getElementById(id).innerHTML = data;
            })
            .fail(
                function (jqXHR, textStatus) {
                    alert("Não foi possível obter o número de batalhas" +
                        "\nCódigo de Erro: " + jqXHR.status + "\n\n" + jqXHR.responseText);
                });
    }
})