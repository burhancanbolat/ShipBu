
    
    // Sayfa yüklendiğinde otomatik olarak çağrılsın

    function submitForm() {
            var boxProductId = document.querySelector('input[name="ProductFeature.BoxProductId"]').value;
    window.location.href = '/LocationInfo?BoxProductId=' + encodeURIComponent(boxProductId);
    // Checkbox durumlarını kontrol et
    var checkboxes = document.querySelectorAll('.form-check-input');

    checkboxes.forEach(function (checkbox) {
                // Checkbox işaretli ise
                if (checkbox.checked) {
                    // İlgili checkbox'a ait feature Id değerini al
                    var featureId = checkbox.value;

    // Feature Id değerini ilgili input alanına at
    document.querySelector('input[name="ProductFeature.BoxProductId"]').value = featureId;

                }
            });

    // Formu gönder
    document.querySelector('form').submit();
        }


