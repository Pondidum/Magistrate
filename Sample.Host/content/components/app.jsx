var App = React.createClass({

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
      tileSize: reactCookie.load('tileSize') || Tile.sizes.large,
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
    this.loadUsers();
  },

  setTileSize(size) {
    this.setState({ tileSize: size });
    reactCookie.save('tileSize', size);
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

  loadUsers() {
    $.ajax({
      url: "/api/users/all",
      dataType: 'json',
      cache: false,
      success: function(data) {
        this.setState({
          users: data || []
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
    this.setState({
      users: collection.add(this.state.users, item)
    });
  },

  onRemoveUser(item) {
    this.setState({
      users: collection.remove(this.state.users, item)
    });
  },

  onChangeUsers(added, removed) {
    this.setState({
      users: collection.change(this.state.users, added, removed)
    });
  },

  render() {

    var tileSize = this.state.tileSize;

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
            tileSize={tileSize}
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
            tileSize={tileSize}
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
            tileSize={tileSize}
          />
        );

        break;

      case 'users':

        selected = 'users';
        content = (
          <UserOverview
            collection={this.state.users}
            onAdd={this.onAddUser}
            onRemove={this.onRemoveUser}
            onChange={this.onChangeUsers}
            navigate={this.navigate}
            url="/api/users"
            tileSize={tileSize}
          />
        );

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
            tileSize={tileSize}
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
            tileSize={tileSize}
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
        <MainMenu navigate={this.navigate} selected={selected} tileSize={tileSize} setTileSize={this.setTileSize} />
        <div className="">
          {content}
        </div>
      </div>
    );
  }

});
