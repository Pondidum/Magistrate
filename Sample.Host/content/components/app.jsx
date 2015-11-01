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

    switch (this.state.location.name) {
      case 'users':
        content = (<ManageUsersView navigate={this.navigate} />);
        break;
      case 'singleuser':
        content = (<UserView id={this.state.location.options.key} />);
        break;
      case 'roles':
        content = (<h1>Roles</h1>);
        break;
      case 'permissions':
        content = (<h1>Permissions</h1>);
        break;
    }

    return (
      <div className="row">
        <MainMenu navigate={this.navigate} selected={this.state.location.name} />
        <div className="col-md-9">
          {content}
        </div>
      </div>
    );
  }

});
