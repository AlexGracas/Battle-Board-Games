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
    .fail();
function criarJogo() {
    $.ajax({
        type: 'GET',
        url: baseURL +
            "/api/BatalhasAPI/CriarBatalha"
    })
        .done(
            function (data) {
                window.location.href = "/Batalhas/Lobby/" + data.id;
            }
        )
        .fail(
            function () {
                alert("Erro ao Criar a Batalha.")
            });
}