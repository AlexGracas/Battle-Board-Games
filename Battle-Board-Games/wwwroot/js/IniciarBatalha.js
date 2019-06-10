var baseURL = window.location.protocol +
    "//" + window.location.hostname +
    (window.location.port ? ':'
        + window.location.port : '');

function iniciarBatalha(idBatalha) {
    $.ajax({
        type: 'GET',
        url: baseURL +
            "/api/BatalhasAPI/IniciarBatalha/" + idBatalha
    })
        .done(
        function (data) {
            window.location.href = "/Batalhas/Tabuleiro/" + idBatalha
            }
        )
        .fail()
}