var App = React.createClass({

  router: unirouter({
    users: 'GET /users',
    singleuser: 'GET /users/:key',
    roles: 'GET /roles',
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
  },

  render() {

    var content;
    var selected = '';

    switch (this.state.location.name) {

      case 'singleuser':
        var key = this.state.location.options.key;

        content = (<SingleUserView id={key} key={key} />);
        selected = 'users';

        break;

      case 'users':

        content = (<UserOverview navigate={this.navigate} />);
        selected = 'users';

        break;

      case 'roles':
        content = (<RoleOverview navigate={this.navigate} />);
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
        <div className="col-md-9">
          {content}
        </div>
      </div>
    );
  }

});
