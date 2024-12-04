
   
$(document).ready(function () {
    $("#urunleriListeleBtn").click(function () {
        cancelRequest = false;
        startTime = new Date();

        // Buton durumlarını güncelle
        $("#urunleriListeleBtn").prop("disabled", true);
        $("#iptalBtn").show();
        $("#statusMessage").text("Listeleme işlemi başladı...");

        // AJAX isteği
        ajaxRequest = $.ajax({
            url: '/Urunler/UrunListele', // Razor yerine doğrudan URL
            type: 'POST',
            beforeSend: function () {
                if (cancelRequest) {
                    return false;
                }
            },
            success: function (data) {
                if (cancelRequest) return;

                $("#urunListesi").empty();
                if (data.message) {
                    $("#urunListesi").append("<p>" + data.message + "</p>");
                } else {
                    data.forEach(function (urun) {
                        $("#urunListesi").append("<p>" + urun.ad + " - " + urun.fiyat + " TL</p>");
                    });
                }

                var endTime = new Date();
                var duration = (endTime - startTime) / 1000;
                $("#statusMessage").text("Listeleme işlemi tamamlandı. Geçen süre: " + duration + " saniye.");
            },
            complete: function () {
                $("#urunleriListeleBtn").prop("disabled", false);
                $("#iptalBtn").hide();
            },
            error: function () {
                if (cancelRequest) return;
                $("#urunListesi").append("<p>Veriler alınırken hata oluştu.</p>");
                var endTime = new Date();
                var duration = (endTime - startTime) / 1000;
                $("#statusMessage").text("Veri alınırken hata oluştu. Geçen süre: " + duration + " saniye.");
            }
        });
    });

    $("#iptalBtn").click(function () {
        cancelRequest = true;

        // AJAX isteğini iptal et
        if (ajaxRequest) {
            ajaxRequest.abort();
        }

        $("#urunListesi").append("<p>İşlem iptal edildi.</p>");
        var endTime = new Date();
        var duration = (endTime - startTime) / 1000;
        $("#statusMessage").text("İşlem iptal edildi. Geçen süre: " + duration + " saniye.");
        $("#urunleriListeleBtn").prop("disabled", false);
        $("#iptalBtn").hide();

        // Sunucu tarafına iptal sinyali gönder
        $.ajax({
            url: '/Urunler/CancelRequest',
            type: 'POST'
        });
    });
});
