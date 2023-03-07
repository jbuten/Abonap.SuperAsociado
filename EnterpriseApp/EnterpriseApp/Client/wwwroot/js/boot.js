/*
	Boot by TEMPLATE STOCK
	templatestock.co @templatestock
	Released for free under the Creative Commons Attribution 3.0 license (templatestock.co/license)
*/

//custom js file for appstartup
(function() {

  var AppStartup = {
    
    init: function() {
      this.phoneAffix();
      this.owlCarousel();
      this.magnificPopup();
      this.loadMap();
    },

    phoneAffix : function() {
      $('#top-phone').affix({
        offset: {
          top: 0,
          bottom: function(){
            var docHeight = $('body').outerHeight(true);
            var jumbotronHeight = $('.jumbotron-container').outerHeight(true);
            var featuresHeight = $('#features').outerHeight(true);
            return docHeight - (jumbotronHeight + featuresHeight) + 100;
          },
        }
      });
    },

    owlCarousel : function() {
      
      $("#reviews-carousel").owlCarousel({
        autoPlay: 5000,
        singleItem:true
      });

      $("#screens-carousel").owlCarousel({
        items : 4,
        itemsCustom : false,
        autoPlay: 5000,
      });

      $("#blog-carousel").owlCarousel({
        items : 4,
        itemsCustom : false,
        autoPlay: 10000,
      });

      $("#prices-carousel").owlCarousel({
          itemsCustom: [[0, 1], [600, 2], [768, 2], [992, 3], [1200, 4]],
          autoPlay: 7000,
      });

      $("#team-carousel").owlCarousel({
          itemsCustom: [[0, 1], [600, 2], [992, 2], [1200, 3]],
          autoPlay: 15000,
      });

      $("#logo-carousel").owlCarousel({
          items : 4,
          itemsCustom : false,
          autoPlay: 3000,
      });
    },

    magnificPopup : function() {

      $('#screens-carousel').magnificPopup({
        delegate: 'a', 
        type: 'image',
        mainClass: 'mfp-no-margins mfp-with-zoom',
        closeOnContentClick: false,
        closeBtnInside: false,
        gallery:{
          enabled:true
        },
        zoom: {
          enabled: true,
          duration: 300,
          opener: function(element) {
            return element.find('img');
          }
        }
      });

      $('#video-popup').magnificPopup({
        disableOn: 700,
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,

        fixedContentPos: false
      });
    },

    loadMap : function() {
      // The latitude and longitude of your business / place
      var position = [18.4669883, -69.9358878];

      var styles = [{"featureType":"landscape","elementType":"labels","stylers":[{"visibility":"off"}]}, {"featureType":"transit","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"poi","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"water","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"road","elementType":"labels.icon","stylers":[{"visibility":"off"}]},{"stylers":[{"hue":"#00aaff"},{"saturation":-100},{"gamma":2.15},{"lightness":12}]},{"featureType":"road","elementType":"labels.text.fill","stylers":[{"visibility":"on"},{"lightness":24}]},{"featureType":"road","elementType":"geometry","stylers":[{"lightness":57}]}];
       
      function showGoogleMaps() {
       
        var latLng = new google.maps.LatLng(position[0], position[1]);

        var mapOptions = {
          scrollwheel: false,
          zoom: 16,
          zoomControl: true,
          streetViewControl: false,
          scaleControl: false,
          mapTypeId: google.maps.MapTypeId.ROADMAP,
          center: latLng
        };

        map = new google.maps.Map(document.getElementById('google-map'), mapOptions);

        // Show the default red marker at the location
        marker = new google.maps.Marker({
          position: latLng,
          map: map,
          draggable: false,
          animation: google.maps.Animation.DROP
        });

        map.setOptions({
          styles: styles
        });
      }
       
      google.maps.event.addDomListener(window, 'load', showGoogleMaps);

    },
  }

  AppStartup.init();

})();


$(document).ready(function() {
  
  $(".nav a").click(function() {
    $('.navmenu').offcanvas('hide');
    $('.navbar-toggle').removeClass("active");
  });

  $('body').on('show.bs.offcanvas', function() {
    $(".navbar-toggle").addClass("active");
  });

  $('body').on('hide.bs.offcanvas', function() {
    $(".navbar-toggle").removeClass("active");
  });


  //open intrest point description
  $('.cd-single-point').children('a').on('click', function(){
    
    var selectedPoint = $(this).parent('li');
    
    if( selectedPoint.hasClass('is-open') ) {
      selectedPoint.removeClass('is-open').addClass('visited');
    } else {
      selectedPoint.addClass('is-open').siblings('.cd-single-point.is-open').removeClass('is-open').addClass('visited');
    }
  });

  //close interest point description
  $('.cd-close-info').on('click', function(event) {
    event.preventDefault();
    $(this).parents('.cd-single-point').eq(0).removeClass('is-open').addClass('visited');
  });  

});