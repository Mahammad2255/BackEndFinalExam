$(document).ready(function () {
	$(document).on("click", ".productdetail", function (e) {
		e.preventDefault();

		let url = $(this).attr("href");

		fetch(url).then(response => response.text())
			.then(data => {
				$(".modal-content").html(data)

				$('.product-large-slider').slick({
					fade: true,
					arrows: false,
					asNavFor: '.pro-nav'
				});


				// product details slider nav active
				$('.pro-nav').slick({
					slidesToShow: 4,
					asNavFor: '.product-large-slider',
					arrows: false,
					focusOnSelect: true
				});

				// testimonial carousel active js
				$('.testimonial-active').slick({
					dots: true,
					arrows: false,
					responsive: [
						{
							breakpoint: 992,
							settings: {
								dots: false
							}
						}
					]
				});


				// image zoom effect
				$('.img-zoom').zoom();

				var rangeSlider = $(".price-range"),
					amount = $("#amount"),
					minPrice = rangeSlider.data('min'),
					maxPrice = rangeSlider.data('max');
				rangeSlider.slider({
					range: true,
					min: minPrice,
					max: maxPrice,
					values: [minPrice, maxPrice],
					slide: function (event, ui) {
						amount.val("$" + ui.values[0] + " - $" + ui.values[1]);
					}
				});
				amount.val(" $" + rangeSlider.slider("values", 0) +
					" - $" + rangeSlider.slider("values", 1));

				$('.product-view-mode a').on('click', function (e) {
					e.preventDefault();
					var shopProductWrap = $('.shop-product-wrap');
					var viewMode = $(this).data('target');
					$('.product-view-mode a').removeClass('active');
					$(this).addClass('active');
					shopProductWrap.removeClass('grid-view list-view').addClass(viewMode);
				})


				// quantity change js
				$('.pro-qty').prepend('<span class="dec qtybtn">-</span>');
				$('.pro-qty').append('<span class="inc qtybtn">+</span>');
				$('.qtybtn').on('click', function () {
					var $button = $(this);
					var oldValue = $button.parent().find('input').val();
					if ($button.hasClass('inc')) {
						var newVal = parseFloat(oldValue) + 1;
					} else {
						// Don't allow decrementing below zero
						if (oldValue > 0) {
							var newVal = parseFloat(oldValue) - 1;
						} else {
							newVal = 0;
						}
					}
					$button.parent().find('input').val(newVal);
				});



				$("#quick_view").modal("show")


			})
	})

	$(document).on("click", "#addbasketbtn", function (e) {


		e.preventDefault()
	
		let url = $("#basketform").attr("action")
		let count = $("#productcount").val();
		url = url + "?count=" + count;
		fetch(url).then(response => {
			return response.text();
		}).then(data => {
			$(".minicart-inner-content").html(data)

        })
	})

	$(document).on("click", ".addbasketlink", function (e) {


		e.preventDefault()

		let url = $(this).attr("href")

		fetch(url).then(response => {
			return response.text();
		}).then(data => {
			$(".minicart-inner-content").html(data)

		})
	})

	$(document).on("click", ".basketUpdate", function (e) {
		e.preventDefault();
		let url = $(this).attr("href");
		let count = $(this).parent().children()[1].value;
		count = parseInt(count);

		if ($(this).hasClass("subCount")) {
			count--;
		}
		else if ($(this).hasClass("addCount")) {
			count++;
		}
		$(this).parent().children()[1].value = count
		url = url + "?count=" + count;

		fetch(url).then(response => {
			fetch("Basket/GetBasket").then(response => response.text()).then(data => $(".header-cart").html(data))
			return response.text()
		}).then(data => $(".basketContainer").html(data))
	})
	$(document).on("keyup", ".basketItemCount", function () {
		let url = $(this).next().attr("href");
		url = url + "?count=" + $(this).val();

		if ($(this).val().trim()) {
			fetch(url).then(response => response.text()).then(data => $(".basketContainer").html(data))

		}
	})
})