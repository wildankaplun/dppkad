/*!
 * jQuery Vertical News Ticker Plugin
 *
 * http://www.jugbit.com/jquery-vticker-vertical-news-ticker/
 * http://github.com/kasp3r/vTicker
 *
 * Copyright 2013 Tadas Juozapaitis
 * Released under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 * Modified By Wildan Kaplun
 * Copyright 2016
 */
(function ($) {
    $.fn.vTicker = function (options) {
        var defaults = {
            speed: 700,
            pause: 4000,
            showItems: 2,
            animation: '',
            mousePause: true,
            isPaused: false,
            direction: 'up',
            height: 0
        };

        var options = $.extend(defaults, options);

        moveUp = function (obj2, height, options) {
            if (options.isPaused)
                return;

            var obj = obj2.children('tr');

            var clone = obj.children('tr:first').clone(true);

            if (options.height > 0) {
                height = obj.children('tr:first').height();
            }

            obj.animate({ top: '-=' + height + 'px' }, options.speed, function () {
                $(this).children('tr:first').remove();
                $(this).css('top', '0px');
            });

            if (options.animation == 'fade') {
                obj.children('tr:first').fadeOut(options.speed);
                if (options.height == 0) {
                    obj.children('tr:eq(' + options.showItems + ')').hide().fadeIn(options.speed).show();
                }
            }

            clone.appendTo(obj);
        };

        moveDown = function (obj2, height, options) {
            if (options.isPaused)
                return;

            var obj = obj2.children('tr');

            var clone = obj.children('tr:last').clone(true);

            if (options.height > 0) {
                height = obj.children('tr:first').height();
            }

            obj.css('top', '-' + height + 'px')
                .prepend(clone);

            obj.animate({ top: 0 }, options.speed, function () {
                $(this).children('tr:last').remove();
            });

            if (options.animation == 'fade') {
                if (options.height == 0) {
                    obj.children('tr:eq(' + options.showItems + ')').fadeOut(options.speed);
                }
                obj.children('tr:first').hide().fadeIn(options.speed).show();
            }
        };

        return this.each(function () {
            var obj = $(this);
            var maxHeight = 0;

            obj.css({ overflow: 'hidden', position: 'relative' })
                .children('tr').css({ position: 'absolute', margin: 0, padding: 0 })
                .children('td').css({ margin: 0, padding: 0 });

            if (options.height == 0) {
                obj.children('tr').children('td').each(function () {
                    if ($(this).height() > maxHeight) {
                        maxHeight = $(this).height();
                    }
                });

                obj.children('tr').children('td').each(function () {
                    $(this).height(maxHeight);
                });

                obj.height(maxHeight * options.showItems);
            }
            else {
                obj.height(options.height);
            }

            var interval = setInterval(function () {
                if (options.direction == 'up') {
                    moveUp(obj, maxHeight, options);
                }
                else {
                    moveDown(obj, maxHeight, options);
                }
            }, options.pause);

            if (options.mousePause) {
                obj.bind("mouseenter", function () {
                    options.isPaused = true;
                }).bind("mouseleave", function () {
                    options.isPaused = false;
                });
            }
        });
    };
})(jQuery);