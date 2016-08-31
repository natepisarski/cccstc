function translateMonth(monthName) {
	var monthMap = {
		"January" : 1,
		"February" : 2,
		"March" : 3,
		"April" : 4,
		"May" : 5,
		"June" : 6,
		"July" : 7,
		"August" : 8,
		"September" : 9,
		"October" : 10,
		"November": 11,
		"December" : 12};

		return monthMap[monthName];
}

function formatDate(month, day, year, hour, minute, second, AM_PM) {
// Wants this format: 8/24/2016 7:57:35 PM
	return translateMonth(month) + "/" + day + "/" + year + " " + hour + ":" + minute + ":" + second + " " + AM_PM;
}

function formatDateWithTime(month, day, year, time, AM_PM) {
	return translateMonth(month) + "/" + day + "/" + year + " " + time + " " + AM_PM;
}

function loadSessions(fromMonth, fromDay, toMonth, toDay)
{
	var fMonth = fromMonth.options[fromMonth.selectedIndex].text;
	var fDay = fromDay.options[fromDay.selectedIndex].text;

	var tMonth = toMonth.options[toMonth.selectedIndex].text;
	var tDay = toDay.options[toDay.selectedIndex].text;
	var emailBox = document.getElementById("email");

	// POST variables
	var fromFormat = formatDate(fMonth, fDay, new Date().getFullYear(), "12", "00", "00", "AM");
	var toFormat = formatDate(tMonth, tDay, new Date().getFullYear(), "11", "00", "00", "PM");
	var email = emailBox.options[emailBox.selectedIndex].text;

	var ajaxRequest = $.ajax({
			method: "POST",
			url: "/Session/GetSessions",
			dataType: "html",
			data: {
				fromFormat : fromFormat,
				toFormat : toFormat,
				email: email
			}
		});

	ajaxRequest.done(function(html) { $("#tableArea").replaceWith(html);});
}

function submitDateQuery(email, fromMonth, fromDay, fromTime, fromAM_PM, toMonth, toDay, toTime, toAM_PM)
{
	var fMonth = fromMonth.options[fromMonth.selectedIndex].text;
	var fDay = fromDay.options[fromDay.selectedIndex].text;
	
	var tMonth = toMonth.options[toMonth.selectedIndex].text;
	var tDay = toDay.options[toDay.selectedIndex].text;

	var fTime = fromTime.value;
	var tTime = toTime.value;

	var fAM_PM = fromAM_PM.options[fromAM_PM.selectedIndex].text;
	var tAM_PM = toAM_PM.options[toAM_PM.selectedIndex].text;

	var user = email.options[email.selectedIndex].text;

	// POST variables
	var fromFormat = formatDateWithTime(fMonth, fDay, new Date().getFullYear(), fTime, fAM_PM);
	var toFormat = formatDateWithTime(tMonth, tDay, new Date().getFullYear(), tTime, tAM_PM);

	var ajaxRequest = $.ajax({
			method: "POST",
			url: "/Session/WriteSession",
			dataType: "html",
			data: {
				fromFormat : fromFormat,
				toFormat : toFormat,
				email: user
			}
		});

	ajaxRequest.done(function(html) { $("#message").replaceWith(html);});
}