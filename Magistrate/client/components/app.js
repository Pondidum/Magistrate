import React from 'react'
import unirouter from 'uniloc'
import cookie from 'react-cookie'
import moment from 'moment'

import MainMenu from './mainmenu'

const App = React.createClass({

  componentDidMount() {
    window.addEventListener('hashchange', this.navigated, false);

    $(document).ajaxError(function(e, xhr, settings, err) {
      console.error(settings.url, err.toString());
    });

    var locale = window.navigator.userLanguage || window.navigator.language;
    moment.locale(locale);
  },

  render() {
    return (
      <div className="row">
        <MainMenu route={this.props.route.childRoutes} />
        <div className="">
          {this.props.children}
        </div>
      </div>
    );
  }

});

export default App
