var App = App || {};
(function () {

    var appLocalizationSource = abp.localization.getSource('IFoxtec');
    App.localize = function () {
        return appLocalizationSource.apply(this, arguments);
    };

})(App);