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
      tileSize: Tile.sizes.large
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

  },

  setTileSize(size) {
    this.setState({ tileSize: size });
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

        content = (<UserOverview navigate={this.navigate} url="/api/users" tileSize={tileSize} />);
        selected = 'users';

        break;

      case 'roles':
        content = (<RoleOverview navigate={this.navigate} url="/api/roles" tileSize={tileSize} />);
        selected = 'roles';

        break;

      case 'permissions':
        content = (<PermissionOverview navigate={this.navigate} url="/api/permissions" tileSize={tileSize} />);
        selected = 'permissions';

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
