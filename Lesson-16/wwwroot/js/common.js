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