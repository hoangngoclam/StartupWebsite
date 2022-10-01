document.addEventListener("DOMContentLoaded", function () {
	var numSubscribeText = document.getElementById("numSubscribe");	
	var numSubscribe = parseInt(numSubscribeText.textContent);
	var btnSubscribe = document.getElementById("btnSubscribe");
	btnSubscribe.addEventListener("click", function () {
		//Check button is a subcribed button 
		if (btnSubscribe.classList.contains("btn-subscribed")) {
			//Decrease number subcribe
			
			$.ajax({
				type: "POST",
				url: "/StartupLikes/UnSubscribe/" + this.dataset.id,
				data: "data",
				dataType: "json",
				success: function (response) {
					if (parseInt(response) == -1) {
						window.location.href = "/dang-nhap"
					}
					else if (parseInt(response) == 1) {
						numSubscribe -= 1;
						btnSubscribe.firstChild.remove();
						btnSubscribe.innerHTML = "Đăng ký";
						btnSubscribe.classList.remove("btn-subscribed");
						numSubscribeText.textContent = numSubscribe;
					}
					else {
						alert("False")
                    }
				}
			});
			//Change ui
			
			
		}
		//Check button is not a subcribed button 
		else {
			//Increase number subcribe
			
			$.ajax({
				type: "POST",
				url: "/StartupLikes/Subscribe/" + this.dataset.id,
				data: "data",
				dataType: "json",
				success: function (response) {
					if (parseInt(response) == -1) {
						window.location.href = "/dang-nhap"
					}
					else if (parseInt(response) == 1) {
						numSubscribe += 1;
						btnSubscribe.innerHTML = `<i class="ti-check"></i>Đã đăng ký`;
						btnSubscribe.classList.add("btn-subscribed");
						numSubscribeText.textContent = numSubscribe;
					}
					else {
						alert("False")
					}
				}
			});
		}
		
	});
});