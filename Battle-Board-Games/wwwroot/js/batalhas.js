var baseURL = window.location.protocol +
    "//" + window.location.hostname +
    (window.location.port ? ':'
        + window.location.port : '');
$.ajax({
    type: 'GET',
    url: baseURL +
        "/api/BatalhasAPI/QtdBatalhas"
})
    .done(
    function (data) {
        document
            .getElementById("qtdBatalhas")
            .innerHTML = data;
    }
    )
    .fail()

$.ajax({
    type: 'GET',
    url: baseURL +
        "/api/BatalhasAPI/QtdBatalhasJogador"
})
    .done(
        function (data) {
            document
                .getElementById("qtdBatalhasJogador")
                .innerHTML = data;
        }
    )
    .fail()