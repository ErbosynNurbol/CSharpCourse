$.$btnStatus=  function($btn,status){
    if(status == 'loading'){
        $btn.prop('disabled',true);
        $btn.attr('data-old-html',$btn.html());
        $btn.html(`<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>`+$btn.attr('data-loading-text'));
    }else {
        $btn.prop('disabled',false);
        $btn.html($btn.attr('data-old-html'));
    }
}

$.$setFormInputStatus =  function($elem,message){
    let $form = $elem.closest('form');
    $form.find('.invalid-feedback').remove();
    $form.find('.is-invalid').removeClass('is-invalid');
    $elem.addClass('is-invalid');
    var $feedback = $('<div class="invalid-feedback">'+message+'</div>');
    $elem.after($feedback);
    $elem.on("change input keyup",function(e){
        if($(this).val()){
            $(this).removeClass('is-invalid');
        }
        $(this).siblings('.invalid-feedback').remove();
    });
}

$.setRecodeTime  = function($btn,time){
    if(time>0){
      $btn.attr('data-old-html',$btn.html());
      $btn.text("Кодты қайта жіберу("+time+"s)");
      $btn.prop("disabled",true);
      setTimeout(()=>{$.setRecodeTime($btn,--time);},1000);
    }else{$btn.prop("disabled",true);
      $btn.html($btn.attr('data-old-html'));
       $btn.prop("disabled",false);
    }
};

function showCustomModal(modal_url, modal_width) {
    modal_url = encodeURI(modal_url);
    $("#custom-modal").remove();
    var modal = $('<div id="custom-modal" class="modal fade emle-modal" tabindex="-1" role="dialog">\
                                <div class="modal-dialog" role="document" style="width:' + modal_width + '!important;">\
                                    <div class="modal-content">\
                                    </div>\
                                </div>\
                        </div>').modal();

    if (modal_width !== '') {
        modal.find(".modal-dialog").addClass('no-max-width');
    } else {
        modal.find(".modal-dialog").removeClass('no-max-width');
    }

    $('body').append(modal);
    modal.find('.modal-content')
        .load(modal_url, function (responseText, textStatus) {
            if (textStatus === 'success' ||
                textStatus === 'notmodified') {
                modal.show();
                if ($(window).width() < 768) {
                    modal.find(".modal-dialog").css("width", "");
                }
            }
        });

    modal.on('hidden.bs.modal', function (e) {
        $("#custom-modal").remove();
    });
}

$(function(){
    $('a[rel="custom-modal"]').on("click",function (e) {
        showCustomModal($(this).attr('href'),($(this).attr('data-width')?$(this).attr('data-width'):''));
        e.preventDefault(); //Stop Defaut Action
    });

});