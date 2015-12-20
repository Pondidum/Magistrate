var App = React.createClass({

  router: unirouter({
    users: 'GET /users',
    singleuser: 'GET /users/:key',
    roles: 'GET /roles',
    singlerole: 'GET /roles/:key',
    permissions: 'GET /permissions',
    singlepermission: 'GET /permissions/:key'
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
    var newCollection = this.state.permissions.concat([item]);

    this.setState({
      permissions: newCollection
    });
  },

  onRemovePermission(item) {

    var newCollection = this.state.permissions.filter(function(x) {
      return x.key !== item.key
    });

    this.setState({
      permissions: newCollection
    });

  },

  onChangePermissions(added, removed) {

    var current = this.state.permissions;

    current = current.concat(added);
    current = current.filter(function(item) {
      return removed.find(p => p.key == item.key) == null;
    });

    this.setState({ permissions: current });
  },

  onAddRole(item) {
    var newCollection = this.state.roles.concat([item]);

    this.setState({
      roles: newCollection
    });
  },

  onRemoveRole(item) {

    var newCollection = this.state.roles.filter(function(x) {
      return x.key !== item.key
    });

    this.setState({
      roles: newCollection
    });

  },

  onChangeRoles(added, removed) {

    var current = this.state.roles;

    current = current.concat(added);
    current = current.filter(function(item) {
      return removed.find(p => p.key == item.key) == null;
    });

    this.setState({ roles: current });
  },

  onAddUser(item) {
    var newCollection = this.state.users.concat([item]);

    this.setState({
      users: newCollection
    });
  },

  onRemoveUser(item) {

    var newCollection = this.state.users.filter(function(x) {
      return x.key !== item.key
    });

    this.setState({
      users: newCollection
    });

  },

  onChangeUsers(added, removed) {

    var current = this.state.users;

    current = current.concat(added);
    current = current.filter(function(item) {
      return removed.find(p => p.key == item.key) == null;
    });

    this.setState({ users: current });
  },

  render() {

    var tileSize = this.state.tileSize;

    var content;
    var selected = '';

    switch (this.state.location.name) {

      case 'singleuser':
        var key = this.state.location.options.key;

        content = (<SingleUserView key={key} userKey={key} url={"/api/users/" + key} navigate={this.navigate} tileSize={tileSize} />);
        selected = 'users';

        break;

      case 'singlerole':
        var key = this.state.location.options.key;

        content = (<SingleRoleView key={key} roleKey={key} url={"/api/roles/" + key} navigate={this.navigate} tileSize={tileSize} />);
        selected = 'roles';

        break;

      case 'singlepermission':
        var key = this.state.location.options.key;

        content = (<SinglePermissionView key={key} permissionKey={key} url={"/api/permissions/" + key} navigate={this.navigate} tileSize={tileSize} />);
        selected = 'permissions';

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
