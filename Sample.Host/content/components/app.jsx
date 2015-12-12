var App = React.createClass({

  router: unirouter({
    users: 'GET /users',
    singleuser: 'GET /users/:key',
    roles: 'GET /roles',
    singlerole: 'GET /roles/:key',
    permissions: 'GET /permissions'
  }),

  getInitialState() {
    return {
      location: this.getLocation()
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

  render() {

    var content;
    var selected = '';

    switch (this.state.location.name) {

      case 'singleuser':
        var key = this.state.location.options.key;

        content = (<SingleUserView userKey={key} key={key} />);
        selected = 'users';

        break;

      case 'singlerole':
        var key = this.state.location.options.key;

        content = (<SingleRoleView key={key} roleKey={key} url={"/api/roles/" + key} />);
        selected = 'roles';

        break;

      case 'users':

        content = (<UserOverview navigate={this.navigate} />);
        selected = 'users';

        break;

      case 'roles':
        content = (<RoleOverview navigate={this.navigate} url="/api/roles" />);
        selected = 'roles';

        break;

      case 'permissions':
        content = (<PermissionOverview navigate={this.navigate} />);
        selected = 'permissions';

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
