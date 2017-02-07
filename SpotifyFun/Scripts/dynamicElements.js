function playPreview(divName, url) {
    document.getElementById(divName).innerHTML = '<iframe type="text/html" width="480" height="75" src='+url+' frameborder="0"></iframe>';
}