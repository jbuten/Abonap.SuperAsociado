// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/**
 * --------------------------------------------
 * AdminLTE Dropdown.js
 * License MIT
 * --------------------------------------------
 */
/**
 * Constants
 * ====================================================
 */

var NAME$4 = 'Dropdown';
var DATA_KEY$4 = 'lte.dropdown';
var JQUERY_NO_CONFLICT$4 = $__default['default'].fn[NAME$4];
var SELECTOR_NAVBAR = '.navbar';
var SELECTOR_DROPDOWN_MENU = '.dropdown-menu';
var SELECTOR_DROPDOWN_MENU_ACTIVE = '.dropdown-menu.show';
var SELECTOR_DROPDOWN_TOGGLE = '[data-toggle="dropdown"]';
var CLASS_NAME_DROPDOWN_RIGHT = 'dropdown-menu-right';
var CLASS_NAME_DROPDOWN_SUBMENU = 'dropdown-submenu'; // TODO: this is unused; should be removed along with the extend?

var Default$3 = {};
/**
 * Class Definition
 * ====================================================
 */

var Dropdown = /*#__PURE__*/function () {
    function Dropdown(element, config) {
        this._config = config;
        this._element = element;
    } // Public


    var _proto = Dropdown.prototype;

    _proto.toggleSubmenu = function toggleSubmenu() {
        this._element.siblings().show().toggleClass('show');

        if (!this._element.next().hasClass('show')) {
            this._element.parents(SELECTOR_DROPDOWN_MENU).first().find('.show').removeClass('show').hide();
        }

        this._element.parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function () {
            $__default['default']('.dropdown-submenu .show').removeClass('show').hide();
        });
    };

    _proto.fixPosition = function fixPosition() {
        var $element = $__default['default'](SELECTOR_DROPDOWN_MENU_ACTIVE);

        if ($element.length === 0) {
            return;
        }

        if ($element.hasClass(CLASS_NAME_DROPDOWN_RIGHT)) {
            $element.css({
                left: 'inherit',
                right: 0
            });
        } else {
            $element.css({
                left: 0,
                right: 'inherit'
            });
        }

        var offset = $element.offset();
        var width = $element.width();
        var visiblePart = $__default['default'](window).width() - offset.left;

        if (offset.left < 0) {
            $element.css({
                left: 'inherit',
                right: offset.left - 5
            });
        } else if (visiblePart < width) {
            $element.css({
                left: 'inherit',
                right: 0
            });
        }
    } // Static
        ;

    Dropdown._jQueryInterface = function _jQueryInterface(config) {
        return this.each(function () {
            var data = $__default['default'](this).data(DATA_KEY$4);

            var _config = $__default['default'].extend({}, Default$3, $__default['default'](this).data());

            if (!data) {
                data = new Dropdown($__default['default'](this), _config);
                $__default['default'](this).data(DATA_KEY$4, data);
            }

            if (config === 'toggleSubmenu' || config === 'fixPosition') {
                data[config]();
            }
        });
    };

    return Dropdown;
}();
/**
 * Data API
 * ====================================================
 */


$__default['default'](SELECTOR_DROPDOWN_MENU + " " + SELECTOR_DROPDOWN_TOGGLE).on('click', function (event) {
    event.preventDefault();
    event.stopPropagation();

    Dropdown._jQueryInterface.call($__default['default'](this), 'toggleSubmenu');
});
$__default['default'](SELECTOR_NAVBAR + " " + SELECTOR_DROPDOWN_TOGGLE).on('click', function (event) {
    event.preventDefault();

    if ($__default['default'](event.target).parent().hasClass(CLASS_NAME_DROPDOWN_SUBMENU)) {
        return;
    }

    setTimeout(function () {
        Dropdown._jQueryInterface.call($__default['default'](this), 'fixPosition');
    }, 1);
});
/**
 * jQuery API
 * ====================================================
 */

$__default['default'].fn[NAME$4] = Dropdown._jQueryInterface;
$__default['default'].fn[NAME$4].Constructor = Dropdown;

$__default['default'].fn[NAME$4].noConflict = function () {
    $__default['default'].fn[NAME$4] = JQUERY_NO_CONFLICT$4;
    return Dropdown._jQueryInterface;
};

alert('ok');