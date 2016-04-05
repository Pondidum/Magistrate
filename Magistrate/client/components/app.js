import React from 'react'
import unirouter from 'uniloc'
import cookie from 'react-cookie'
import moment from 'moment'

import MainMenu from './mainmenu'
import SingleUserView from './users/SingleUserView'
import UserOverview from './users/UserOverview'
import SingleRoleView from './roles/SingleRoleView'
import RoleOverview from './roles/RoleOverview'
import SinglePermissionView from './permissions/SinglePermissionView'
import PermissionOverview from './permissions/PermissionOverview'
import HistoryOverview from './history/HistoryOverview'

const App = React.createClass({

  router: unirouter({
    users: 'GET /users',
    singleuser: 'GET /users/:key',
    roles: 'GET /roles',
    singlerole: 'GET /roles/:key',
    permissions: 'GET /permissions',
    singlepermission: 'GET /permissions/:key',
    history: 'GET /history'
  }),

  getInitialState() {
    return {
      location: this.getLocation(),
      permissions: [],
      roles: [],
      users: []
    };
  },

  getLocation() {
    var location = window.location.hash.replace(/^#\/?|\/$/g, '') || "users";

    return this.router.lookup(location);
  },

  navigated() {
    this.setState({
      location: this.getLocation()
    })
  },

  navigate(routeName, options) {
    window.location.hash = this.router.generate(routeName, options);
  },

  componentDidMount() {
    window.addEventListener('hashchange', this.navigated, false);

    $(document).ajaxError(function(e, xhr, settings, err) {
      console.error(settings.url, err.toString());
    });

    var locale = window.navigator.userLanguage || window.navigator.language;
    moment.locale(locale);

    this.loadPermissions();
    this.loadRoles();
  },

  loadPermissions() {
    $.ajax({
      url: "/api/permissions/all",
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          permissions: data || []
        });
      }.bind(this)
    });
  },

  loadRoles() {
    $.ajax({
      url: "/api/roles/all",
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          roles: data || []
        });
      }.bind(this)
    });
  },

  onAddPermission(item) {
    this.setState({
      permissions: collection.add(this.state.permissions, item)
    });
  },

  onRemovePermission(item) {
    this.setState({
      permissions: collection.remove(this.state.permissions, item)
    });
  },

  onChangePermissions(added, removed) {
    this.setState({
      permissions: collection.change(this.state.permissions, added, removed)
    });
  },

  onAddRole(item) {
    this.setState({
      roles: collection.add(this.state.roles, item)
    });
  },

  onRemoveRole(item) {
    this.setState({
      roles: collection.remove(this.state.roles, item)
    });
  },

  onChangeRoles(added, removed) {
    this.setState({
      roles: collection.change(this.state.roles, added, removed)
    });
  },

  onAddUser(item) {
  },

  onRemoveUser(item) {
  },

  onChangeUsers(added, removed) {
  },

  render() {

    var content;
    var selected = '';

    switch (this.state.location.name) {

      case 'singleuser':
        var key = this.state.location.options.key;

        selected = 'users';
        content = (
          <SingleUserView
            key={key}
            user={this.state.users.find(u => u.key == key)}
            url={"/api/users/" + key}
            navigate={this.navigate}
          />
        );

        break;

      case 'singlerole':
        var key = this.state.location.options.key;

        selected = 'roles';
        content = (
          <SingleRoleView
            key={key}
            role={this.state.roles.find(r => r.key == key)}
            url={"/api/roles/" + key}
            navigate={this.navigate}
          />
        );

        break;

      case 'singlepermission':
        var key = this.state.location.options.key;

        selected = 'permissions';
        content = (
          <SinglePermissionView
            key={key}
            permission={this.state.permissions.find(p => p.key == key)}
            url={"/api/permissions/" + key}
            navigate={this.navigate}
          />
        );

        break;

      case 'users':

        selected = 'users';
        content = (<UserOverview />);

        break;

      case 'roles':
        selected = 'roles';
        content = (
          <RoleOverview
            collection={this.state.roles}
            onAdd={this.onAddRole}
            onRemove={this.onRemoveRole}
            onChange={this.onChangeRoles}
            navigate={this.navigate}
            url="/api/roles"
          />
        );

        break;

      case 'permissions':
        selected = 'permissions';
        content = (
          <PermissionOverview
            collection={this.state.permissions}
            onAdd={this.onAddPermission}
            onRemove={this.onRemovePermission}
            onChange={this.onChangePermissions}
            navigate={this.navigate}
            url="/api/permissions"
          />
        );

        break;

        case 'history':
          selected = 'history';
          content = (
            <HistoryOverview />
          );

        break;
    }

    return (
      <div className="row">
        <MainMenu navigate={this.navigate} selected={selected} />
        <div className="">
          {content}
        </div>
      </div>
    );
  }

});

export default App
