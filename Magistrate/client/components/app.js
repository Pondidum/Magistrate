import React from 'react'
import unirouter from 'uniloc'
import cookie from 'react-cookie'
import moment from 'moment'

import MainMenu from './mainmenu'
// import SingleUserView from './users/SingleUserView'
// import UserOverview from './users/UserOverview'
// import SingleRoleView from './roles/SingleRoleView'
// import RoleOverview from './roles/RoleOverview'
// import SinglePermissionView from './permissions/SinglePermissionView'
// import PermissionOverview from './permissions/PermissionOverview'
// import HistoryOverview from './history/HistoryOverview'

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
        <MainMenu params={this.props.params} />
        <div className="">
          {this.props.children}
        </div>
      </div>
    );
  }

});

export default App
