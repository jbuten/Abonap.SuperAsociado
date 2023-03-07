/*
	Attractive by TEMPLATE STOCK
	templatestock.co @templatestock
	Released for free under the Creative Commons Attribution 3.0 license (templated.co/license)
 */

/* For demo site only */
var _date = new Date();
var _year = _date.getFullYear();
var _month = _date.getMonth() + 2;
if (_month === 13) {
  _month = 1;
  _year = _year + 1;
} else if (_month === 14) {
  _month = 2;
  _year = _year + 1;
}

/* Event time */
var year 	= _year;
var month	= _month;
var day 	= 28;
var hour 	= 23;
var minute 	= 59;
var second 	= 59;

/* Ken Burns effect (true) or static background (false) */
var KenBurns = true; 

/* Google Analitcs */
var googleAnalyticsId = "UA-15035018-34"; 	// Google Analytics ID - e.g. UA-12345678-90