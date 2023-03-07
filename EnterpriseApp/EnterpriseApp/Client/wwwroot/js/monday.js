/*
	Attractive by TEMPLATE STOCK
	templatestock.co @templatestock
	Released for free under the Creative Commons Attribution 3.0 license (templated.co/license)
 */
 
$(document).ready(function() { 

	/* Loader */
	setTimeout(function() {
        $('body').addClass('loaded');
    }, 2000);

	/* Slideshow with Ken Burns effect */
	$(".background").kenburnsy({
		fullscreen: true
	});

	/* Show sidebar */
	$("[data-toggle=offcanvas]").click(function() {
		$(".offcanvas").toggleClass("active");
		$(".canvas").toggleClass("active");
	});

}); 

$(window).resize(function() {
    
});