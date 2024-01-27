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