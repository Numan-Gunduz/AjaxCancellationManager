
$(document).ready(function () {
    let ajaxRequest;
    let cancelRequest = false;
    let startTime;

    $("#urunleriListeleBtn").click(function () {
        cancelRequest = false;
        startTime = new Date();

        $("#urunleriListeleBtn").prop("disabled", true);
        $("#iptalBtn").show();
        $("#statusMessage").text("Listeleme işlemi başladı...");

        ajaxRequest = $.ajax({
            url: '/Urunler/UrunListele',
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

                let endTime = new Date();
                let duration = (endTime - startTime) / 1000;
                $("#statusMessage").text("Listeleme işlemi tamamlandı. Geçen süre: " + duration + " saniye.");
            },
            complete: function () {
                $("#urunleriListeleBtn").prop("disabled", false);
                $("#iptalBtn").hide();
            },
            error: function () {
                if (cancelRequest) return;
                $("#urunListesi").append("<p>Veriler alınırken hata oluştu.</p>");
                let endTime = new Date();
                let duration = (endTime - startTime) / 1000;
                $("#statusMessage").text("Veri alınırken hata oluştu. Geçen süre: " + duration + " saniye.");
            }
        });
    });


    $("#iptalBtn").click(function () {
        cancelRequest = true;

        if (ajaxRequest) {
            ajaxRequest.abort();
        }

        $("#urunListesi").append("<p>İşlem iptal edildi.</p>");
        let endTime = new Date();
        let duration = (endTime - startTime) / 1000;
        $("#statusMessage").text("İşlem iptal edildi. Geçen süre: " + duration + " saniye.");
        $("#urunleriListeleBtn").prop("disabled", false);
        $("#iptalBtn").hide();
    });

    $(window).on('beforeunload', function () {
        cancelRequest = true;

        if (ajaxRequest) {
            ajaxRequest.abort(); 
        }
    });
});













 
//$(document).ready(function () {
//    $("#urunleriListeleBtn").click(function () {
//        cancelRequest = false;
//        startTime = new Date();

      
//        $("#urunleriListeleBtn").prop("disabled", true);
//        $("#iptalBtn").show();
//        $("#statusMessage").text("Listeleme işlemi başladı...");
//        ajaxRequest = $.ajax({
//            url: '/Urunler/UrunListele',
//            type: 'POST',
//            beforeSend: function () {
//                if (cancelRequest) {
//                    return false;
//                }
//            },
//            success: function (data) {
//                if (cancelRequest) return;

//                $("#urunListesi").empty();
//                if (data.message) {
//                    $("#urunListesi").append("<p>" + data.message + "</p>");
//                } else {
//                    data.forEach(function (urun) {
//                        $("#urunListesi").append("<p>" + urun.ad + " - " + urun.fiyat + " TL</p>");
//                    });
//                }

//                var endTime = new Date();
//                var duration = (endTime - startTime) / 1000;
//                $("#statusMessage").text("Listeleme işlemi tamamlandı. Geçen süre: " + duration + " saniye.");
//            },
//            complete: function () {
//                $("#urunleriListeleBtn").prop("disabled", false);
//                $("#iptalBtn").hide();
//            },
//            error: function () {
//                if (cancelRequest) return;
//                $("#urunListesi").append("<p>Veriler alınırken hata oluştu.</p>");
//                var endTime = new Date();
//                var duration = (endTime - startTime) / 1000;
//                $("#statusMessage").text("Veri alınırken hata oluştu. Geçen süre: " + duration + " saniye.");
//            }
//        });
//    });

//    $("#iptalBtn").click(function () {
//        cancelRequest = true;

//        if (ajaxRequest) {
//            ajaxRequest.abort();
//        }

//       $("#urunListesi").append("<p>İşlem iptal edildi.</p>");
//        var endTime = new Date();
//        var duration = (endTime - startTime) / 1000;
//        $("#statusMessage").text("İşlem iptal edildi. Geçen süre: " + duration + " saniye.");
//        $("#urunleriListeleBtn").prop("disabled", false);
//        $("#iptalBtn").hide();

//        $.ajax({
//            url: '/Urunler/CancelRequest',
//            type: 'POST'
//        });
//    });
  
//    $(window).on('beforeunload', function () {
//        cancelRequest = true;

//        if (ajaxRequest) {
//            ajaxRequest.abort();
//        }

//        $.ajax({
//            url: '/Urunler/CancelRequest',
//            type: 'POST'
//        });

//        $("#statusMessage").text("Sayfadan ayrılma sırasında işlem iptal edildi.");
//    });
//});